'use strict';

module.exports = function(sequelize, DataTypes) {
  var Product = sequelize.define("Products", {
    productId: DataTypes.INTEGER,
    name: DataTypes.STRING,
    description: DataTypes.STRING
  }, {
    classMethods: {
      associate: function(models) {
        //Product.hasMany(models.Task)
      }
    }
  });

  return Product;
};