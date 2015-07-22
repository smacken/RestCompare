Rest Compare
Whats the easiest REST infrastructure?

So here is the thought experiment:
- take three languages, javascript, C#, and golang and see how easy a REST API can be implemented.
- notice the differences in ease of development, how close to the metal the approaches are

An example of the differences in controller actions:

Golang controller http handler
<pre><code>
	func ProductsGet(rw http.ResponseWriter, req *http.Request) {
		db := GetDb(req)
		vars := mux.Vars(req)
		id := vars["id"]
		product := models.Product{}
		err := db.First(&product, id).Error
		if err != nil {
			panic(err)
		}
		r := render.New()
		r.JSON(rw, http.StatusOK, product)
	}
</code></pre>

Node controller action
<pre><code>
	exports.show = function(req, res){
  		db.Products.find(req.params.product).success(function(product){
    	res.json(product);
  	});
};
</code></pre>

C# generic action
<pre><code>
	[Route("{id:int}")]
    public T Get(int id)
    {
        return _repository.Get(id);
    }
</code></pre>

