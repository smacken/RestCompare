goweb
=====

golang web server

- has a simple router; api subrouter for rest resources
- middleware config for standard web server
	> cors, logging, security, gzip, auth
- env configuration .env
	> deployment based configuration using .env key:values

request/response binding
- request binding to structs
<pre>
<code>
login := new(LoginRequest)
if binding.Bind(req, login).Handle(res) {
	return
}
</code>
</pre>

-response binding with content negotiation
<pre><code>r.JSON(res, http.StatusOK, login)</code></pre>