var User = require("../models/user");
const crypto = require("crypto");
const jwt = require("jsonwebtoken");

exports.register = async (req, h) => {
    try {
        var user = await User.findOne({"username": req.payload.username});
        if (user) {
            console.log("User exists");
            throw new Error("Username exists");
        }

        const encrypted = crypto.createHmac("sha1", "secret key").update(req.payload.password).digest("base64"); // secret key라는 문자열을 바탕으로 암호화
        console.log(encrypted);

        let userData = {
            username: req.payload.username,
            password: encrypted,
            isadmin: false
        }

        var user = await User.create(userData);
        const userCount = await User.count({});
        var isadmin = userCount == 1 ? true : false;
        if (isadmin) {
            console.log("admin");
            await user.setAdmin();
            // userData.isadmin = true;
            // await User.findOneAndUpdate({"username": userData.username}, {$set: {"isadmin": true}});
        }
        return ({ ok: true, message: "Register successfully", isadmin: isadmin });
    } catch(err) {
        console.error(err);
        return { ok: false, message: "Register failed" };
    }
};

exports.login = async (req, h) => {
    try {
        const username = req.payload.username;
        const password = req.payload.password;
        console.log(username, password);

        var user = await User.findOneByUsername(username);
        if (!user) {
            console.log("User does not exist");
            throw new Error("User does not exist");
        }
        let token;
        if (await user.verify(password)) {
            console.log("Verification ok!");
            token = await jwt.sign(
                {
                    _id: user._id,
                    username: user.username,
                    admin: user.isadmin
                },
                "NeverShareYourSecret",
                {
                    expiresIn: "7d", // d일 이후 토큰 무효화
                    issuer: 'djyang.com', // 발급자
                    subject: "userinfo"
                }
            );
            console.log("Token: " + token);
        } else {
            console.log("Verification failed");
            throw new Error("Verification failed");
        }
        return { ok: true, message: "Logged in successfully", token };
    } catch(err) {
        console.error(err);
        return { ok: false, message: "Login failed", err };
    }
}

exports.list = async (req, h) => {
    return User.find({}).exec().then( user => {
        return { users: user };
    }).catch( err => {
        return { err: err };
    });
}

exports.remove = async (req, h) => {
    return User.findOne({"username": req.params.username}).exec().then( user => { // params: 주소상의 이름
        if (!user) 
            return { err: "User not found" };
        user.remove();
    }).then(data => {
        return { message: "User deleted successfully" };
    }).catch( err => {
        return { err: err };
    });
}