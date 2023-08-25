# Url_Shortener_API

POST - api/Url/short-url -> creates a short url regarding the given original url and returns short url as a response.
GET - api/Url/redirect -> redirects to an original url that matches with the short url given as an input.
GET - api/Url/get-short-url -> takes original url as an input and returns corresponding short url matches with that.
POST - api/Url/pick-custom-short-url -> takes short url hashed portion and original url as an input, do matching between them and returns custom short url.
