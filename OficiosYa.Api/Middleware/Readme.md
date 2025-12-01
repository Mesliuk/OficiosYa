ExceptionMiddleware: logs exceptions and returns a JSON error. Registered in Program.cs via app.UseMiddleware<ExceptionMiddleware>().

Notes:
- Consider adding Serilog or another logging provider for structured logs.
- This middleware returns limited details in non-development environments.