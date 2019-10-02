'use strict';

const Hapi = require("@hapi/hapi");
const mongoose = require("mongoose");
const hapiAuthJWT = require("hapi-auth-jwt2");
const DogController = require("./src/controllers/dog");
const UserController = require("./src/controllers/user");
const MongoDBUrl = "mongodb://localhost:27017/dogapi";
const fs = require("fs"); // File system library

var options = { // private key를 만들어서 그것으로 public key 만듦
    key: fs.readFileSync("./private.pem"),
    cert: fs.readFileSync("./public.pem")
};

const server = new Hapi.Server({
    port:3000,
    host:"localhost",
    tls: options
});

const registerRoutes = () => {
    server.route({
        method: "GET",
        path: "/dogs",
        config: { auth: false },
        handler: DogController.list
    });
    server.route({
        method: "POST",
        path: "/dogs",
        options: {
            handler: DogController.create
        }
    });
    server.route({
        method: "GET",
        path: "/dogs/{id}",
        config: { auth: false },
        handler: DogController.get
    });
    server.route({
        method: "PUT",
        path: "/dogs/{id}",
        options: {
            handler: DogController.update
        }
    });
    server.route({
        method: "DELETE",
        path: "/dogs/{id}",
        options: {
            handler: DogController.remove
        }
    });


    server.route({
        method: "POST",
        path: "/auth/register",
        config: { auth: false },
        handler: UserController.register
    });
    server.route({
        method: "POST",
        path: "/auth/login",
        config: { auth: false },
        handler: UserController.login
    });
    server.route({
        method: "GET",
        path: "/auth/users",
        handler: UserController.list
    });
    server.route({
        method: "DELETE",
        path: "/auth/users/{username}",
        handler: UserController.remove
    })
};

const validateUser = async (decoded, req, h) => {
    console.log("Decoded", decoded);
    if (decoded && decoded.sub)
        return { isValid: true };
    else
        return { isValid: false };
}

const init = async () => {
    await server.register(hapiAuthJWT);
    server.auth.strategy("jwt", "jwt", {
        key: "NeverShareYourSecret",
        validate: validateUser,
        verifyOptions: { algorithms: ["HS256"] }
    });
    server.auth.default('jwt');

    registerRoutes();

    await server.start();
    mongoose.connect(MongoDBUrl, {}).then(() => {
        console.log("Connected to Mongo Server");
    }, err => {
        console.log(err);
    });
    return server;
};

init().then(server => {
    console.log("Server running at: ", server.info.uri);
}).catch(err => {
    console.log(err);
});
