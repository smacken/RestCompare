package main

import (
	"github.com/codegangsta/negroni"
	"github.com/phyber/negroni-gzip/gzip"
	"github.com/rs/cors"
	"github.com/thoas/stats"
	"github.com/unrolled/secure"
	"net/http"
)

func NewApp() *negroni.Negroni {

	secureMiddleware := secure.New(secure.Options{
		//AllowedHosts:          []string{"example.com", "ssl.example.com"},
		//SSLRedirect:           true,
		//SSLHost:               "ssl.example.com",
		SSLProxyHeaders:       map[string]string{"X-Forwarded-Proto": "https"},
		STSSeconds:            315360000,
		STSIncludeSubdomains:  true,
		STSPreload:            true,
		FrameDeny:             true,
		ContentTypeNosniff:    true,
		BrowserXssFilter:      true,
		ContentSecurityPolicy: "default-src 'self'",
		IsDevelopment:         true,
	})

	corsMiddleware := cors.New(cors.Options{
		//AllowedOrigins: []string{"http://foo.com"},
		AllowCredentials: true,
	})

	app := negroni.New()
	app.Use(negroni.NewRecovery())
	app.Use(negroni.NewLogger())
	app.Use(gzip.Gzip(gzip.DefaultCompression))
	//app.Use(negroni.Wrap(api))
	app.Use(negroni.HandlerFunc(secureMiddleware.HandlerFuncWithNext))
	app.Use(stats.New())
	app.Use(corsMiddleware)
	app.Use(negroni.NewStatic(http.Dir("public")))

	return app
}
