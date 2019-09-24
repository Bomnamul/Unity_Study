var Dog = require("../models/dog");

exports.list = (req, h) => {
    return Dog.find({}).exec().then((dog) => {
        return {dogs: dog};
    }).catch((err) => {
        return {"err": err};
    });
};

exports.create = (req, h) => {
    const dogData = {
        name: req.payload.name,
        breed: req.payload.breed,
        age: req.payload.age,
        image: req.payload.image
    };
    return Dog.create(dogData).then((dog) => {
        return {message: "Dog created successfully", dog: dog};
    }).catch((err) => {
        return {err: err};
    });
};
