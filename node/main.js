var express = require('express'),
	resource = require('express-resource'),
	app = express();

var models = require("./models");

var index = app.resource('index', require('./controllers/index'))
var products = app.resource('products', require('./controllers/product'));

app.set('port', process.env.PORT || 3000);

models.sequelize.sync().success(function(){
	app.listen(app.get('port'));
	console.log('listening on :3000');
});
