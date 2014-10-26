'use strict';

module.exports = function(sequelize, DataTypes) {
  var Product = sequelize.define("Products", {
    Id: { type: DataTypes.INTEGER, primaryKey: true, autoIncrement: true},
    Name: DataTypes.STRING,
    Description: DataTypes.STRING,
    createdAt: false,
    updatedAt: false
  }, {
    classMethods: {
      associate: function(models) {
        //Product.hasMany(models.Task)
      }
    }
  });

  return Product;
};