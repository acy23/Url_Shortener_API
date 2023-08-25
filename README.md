### API Endpoints

**POST - api/Url/short-url**
Creates a short URL based on the provided original URL and returns the generated short URL as a response.

**GET - api/Url/redirect**
Redirects to the original URL that corresponds to the given short URL provided as input.

**GET - api/Url/get-short-url**
Takes an original URL as input and returns the corresponding short URL associated with it.

**POST - api/Url/pick-custom-short-url**
Accepts the hashed portion of a short URL and an original URL as input. It matches them and returns a custom short URL if the match is successful.
