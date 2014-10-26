var db = require('../models')

exports.index = function(req, res){
  db.Products.findAll({}).success(function(products){
    res.json(products);
  });
};

exports.new = function(req, res){
  res.send('new product');
};

exports.create = function(req, res){
  db.Products.create(req.body).success(function(product){
    res.json(product);
  });
};

exports.show = function(req, res){
  db.Products.find(req.params.product).success(function(product){
    res.json(product);
  });
};

exports.edit = function(req, res){
  res.send('edit product ' + req.product.title);
};

exports.update = function(req, res){
  res.send('update product ' + req.product.title);
};

exports.destroy = function(req, res){
  db.Products.find(req.params.product).success(function(product){
    product.destroy().success(function(){
      res.json(product);
    });
  });
};

exports.load = function(id, fn){
  db.Products.find(id).success(function(product){

  });
  process.nextTick(function(){
    fn(null, { title: 'products' });
  });
};