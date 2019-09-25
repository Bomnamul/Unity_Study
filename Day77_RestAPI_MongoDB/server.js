'use strict';

const Hapi = require("hapi");
const mongoose = require("mongoose");
const DogController = require("./src/controllers/dog");
const MongoDBUrl = "mongodb://localhost:27017/dogapi";

const server = new Hapi.Server({
    port:3000,
    host:"localhost"
});

const registerRoutes = () => {
    server.route({
        method: "GET",
        path: "/dogs",
        options: {
            handler: DogController.list
        }
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
        options: {
            handler: DogController.get
        }
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
};

const init = async () => {
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
