var Dog = require("../models/dog");

exports.list = (req, h) => {
    return Dog.find({}).exec().then((dog) => {
        return {dogs: dog};
    }).catch((err) => {
        return { "err": err };
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
        return { message: "Dog created successfully", dog: dog };
    }).catch((err) => {
        return { err: err };
    });
};

exports.get = (req, h) => {
    return Dog.findById(req.params.id).exec().then((dog) => {
        if (!dog)
            return { message: "Dog not found" };
        return { dog: dog };
    }).catch((err) => {
        return { "err": err };
    });
};

exports.update = (req, h) => {
    return Dog.findById(req.params.id).exec().then((dog) => {
        if (!dog)
            return { message: "Dog not found" };

        dog.name = req.payload.name;
        dog.breed = req.payload.breed;
        dog.age = req.payload.age;
        dog.image = req.payload.image;
        dog.save();
    }).then((data) => {
        return { message: "Dog data updated successfully" };
    }).catch((err) => {
        return { "err": err };
    });
};

exports.remove = (req, h) => {
    return Dog.findById(req.params.id).exec().then((dog) => {
        if (!dog)
            return { message: "Dog not found" };
        dog.remove();
    }).then((data) => {
        return { message: "Dog data deleted successfully" };
    }).catch((err) => {
        return { "err": err };
    });
};
