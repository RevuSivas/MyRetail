# MyRetail

1. Created MyRetail restful service using ASP.Net WebAPI framework in c#
2. Added Get endpoint to read product.
   * Read ProductName from Target endpoint url
   * Read Price from Sql Server
   * Mapped ProductName and Price to Product Model
   * Send the Product response back
3. Added Unit testing for Success and failure testcases for Get end point
4. Added PUT endpoint to update price
   * PUT request sending ProductId and Price
   * Using RDDBMS with sql connection
   * updated price by productId
   
5. Added Unite testing for PUt end point.
6. Moved URL and connectionstring to webconfig file.
7. Used POSTMan to test both endpoints.
