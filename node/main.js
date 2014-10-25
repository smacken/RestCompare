var express = require('express'),
	resource = require('express-resource'),
	app = express();

var index = app.resource('index', require('./controllers/index'))
var products = app.resource('products', require('./controllers/product'));

app.listen(3000);
console.log('listening on :3000');