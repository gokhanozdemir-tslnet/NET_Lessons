var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();





app.UseRouting(); //!? Route matching ekler.
#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(
    endpoints =>
    {
        //? ******Biricinci Route***************************************/
        endpoints.MapGet("routing",
            async context =>
            {
                await context.Response.WriteAsync("Request was routed");
            }
        );
        //? ******end of Biricinci Route***************************************/
        //? ******ikinci Route***************************************/
        app.MapGet("{first}/{second}/{third}", async context =>
        {
            await context.Response.WriteAsync("Request was routed\n");
            foreach (var item in context.Request.RouteValues)
            {
                await context.Response.WriteAsync($"Key:{item.Key} - Value:{item.Value} \n");
            }
        });
        //? ******Biricinci Route***************************************/
        //? ******���nc� Route***************************************/
        //matching multiple values from a single URL Segment
        app.MapGet("{first}/{second}.{third}", async context =>
                {
                    await context.Response.WriteAsync("Request was routed\n");
                    foreach (var kvp in context.Request.RouteValues)
                    {
                        await context.Response.WriteAsync($"Key:{kvp.Key} - Value={kvp.Value} \n");
                    }
                }

            );

        //? ******���nc� Route***************************************/

        //Catchall Segment Variable
        app.MapGet("{first}/{second}/{*catchall}", async context =>
        {
            await context.Response.WriteAsync("Request Was Routed\n");
            foreach (var kvp in context.Request.RouteValues)
            {
                await context.Response.WriteAsync($"{kvp.Key}: {kvp.Value}\n");
            }
        });

        //? ******4 Route***************************************/
        //constrainting Segment
        app.MapGet("{first:int}/{second:bool}", async context =>
        {
            await context.Response.WriteAsync("Request was Routed\n");
            foreach (var item in context.Request.RouteValues)
            {
                await context.Response.WriteAsync($"key:{item.Key} value:{item.Value} \n");
            }
        });
        //? ******4 Route***************************************/
        /*
         Constraint Description
        alpha bool
        datetime
        decimal double file
        float guid
        int length(len)
        This constraint matches the letters a to z (and is case-insensitive). This constraint matches true and false (and is case-insensitive).
        This constraint matches DateTime values, expressed in the nonlocalized invariant culture format.
        This constraint matches decimal values, formatted in the nonlocalized invariant culture. This constraint matches double values, formatted in the nonlocalized invariant culture.
        This constraint matches segments whose content represents a file name, in the form name.ext. The existence of the file is not validated.
        This constraint matches float values, formatted in the nonlocalized invariant culture. This constraint matches GUID values. This constraint matches int values.
        This constraint matches path segments that have the specified number of characters.
        length(min, max) This constraint matches path segments whose length falls between the lower and upper values specified.
        long max(val) maxlength(len) min(val) minlength(len) nonfile This constraint matches long values.
        This constraint matches path segments that can be parsed to an int value that is less than or equal to the specified value.
        This constraint matches path segments whose length is equal to or less than the specified value.
        This constraint matches path segments that can be parsed to an int value that is more than or equal to the specified value.
        This constraint matches path segments whose length is equal to or more than the specified value.
        This constraint matches segments that do not represent a file name, i.e., values that would not be matched by the file constraint.
        range(min, max) This constraint matches path segments that can be parsed to an int value that falls between the inclusive range specified.
        regex(expression) This constraint applies a regular expression to match path segments.
        */

    }

);
#pragma warning restore ASP0014 // Suggest using top level route registrations

app.MapGet("/", () => "Hello World!");

app.Run();
