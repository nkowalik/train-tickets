using Microsoft.AspNetCore.Builder;

namespace TrainTicketMachine.Api;

internal class Startup
{
    public void Configure(IApplicationBuilder app)
    {
        app.UseHttpsRedirection();
        app.UseSwagger();
        app.UseRouting();
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => {
            endpoints.MapControllers();
        });
    }
}
