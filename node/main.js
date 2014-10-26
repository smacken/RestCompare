var express = require('express'),
	resource = require('express-resource'),
	bodyParser = require('body-parser'),
	app = express();

var models = require("./models");

var index = app.resource('index', require('./controllers/index'))
var products = app.resource('products', require('./controllers/product'));

app.set('port', process.env.PORT || 3000);
app.use(bodyParser.json());

models.sequelize.sync().success(function(){
	app.listen(app.get('port'));
	console.log('listening on :3000');
});
