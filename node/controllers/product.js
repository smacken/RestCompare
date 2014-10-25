var db = require('../models')

exports.index = function(req, res){
  db.Products.findAll({}).success(function(products){
    res.send(products);
  });
};

exports.new = function(req, res){
  res.send('new product');
};

exports.create = function(req, res){
  res.send('create product');
};

exports.show = function(req, res){
  res.send('show product ' + req.product.title);
};

exports.edit = function(req, res){
  res.send('edit product ' + req.product.title);
};

exports.update = function(req, res){
  res.send('update product ' + req.product.title);
};

exports.destroy = function(req, res){
  res.send('destroy product ' + req.product.title);
};

exports.load = function(id, fn){
  process.nextTick(function(){
    fn(null, { title: 'products' });
  });
};