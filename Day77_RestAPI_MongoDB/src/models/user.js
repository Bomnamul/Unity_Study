"use strict";

const mongoose = require("mongoose");
const Schema = mongoose.Schema;
const crypto = require("crypto");

const userModel = new Schema({
    username: { type: String, required: true, index: {unique: true} },
    password: { type: String, required: true },
    isadmin: { type: Boolean, required: true, default: false }
});

userModel.statics.findOneByUsername = function(username) {
    return this.findOne({username});
}

userModel.methods.verify = function(password) {
    const encrypted = crypto.createHmac("sha1", "secret key").update(password).digest("base64");
    return this.password == encrypted;
}

userModel.methods.setAdmin = function() {
    this.isadmin = true;
    return this.save();
}

module.exports = mongoose.model("User", userModel, "users");
