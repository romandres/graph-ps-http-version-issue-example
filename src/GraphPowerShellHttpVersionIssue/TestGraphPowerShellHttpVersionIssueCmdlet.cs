using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Security;
using Microsoft.Graph;

namespace GraphPowerShellHttpVersionIssue
{
    [Cmdlet(VerbsDiagnostic.Test, "GraphPowerShellHttpVersionIssue")]
    public class TestGraphPowerShellHttpVersionIssueCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public string ApplicationId { get; set; }

        [Parameter(Mandatory = true)]
        public SecureString ApplicationSecret { get; set; }

        [Parameter(Mandatory = true)]
        public string TenantId { get; set; }

        protected override void EndProcessing()
        {
            try
            {
                var appCredential = new NetworkCredential(ApplicationId, ApplicationSecret);
                var graphClient = (new GraphHelper(TenantId, appCredential.UserName, appCredential.Password))
                    .CreateGraphServiceClientAsync()
                    .GetAwaiter()
                    .GetResult(); ;

                // This works with Graph v3.x and PowerShell as well as Windows PowerShell
                // This works with Graph v4.x and PowerShell but not with Windows PowerShell

                var groups = graphClient
                    .Groups
                    .Request()
                    .GetAsync()
                    .GetAwaiter()
                    .GetResult()
                    .Select(group => group.DisplayName)
                    .ToList();

                WriteObject(groups);
            }
            catch (ServiceException ex)
            {
                // The interesting error message is on the inner exception
                if (ex.InnerException?.Message is object && ex.InnerException.Message.Contains("HTTP/1.0"))
                {
                    WriteWarning(ex.InnerException.Message);
                }

                ThrowTerminatingError(new ErrorRecord(ex, "", ErrorCategory.NotSpecified, null));
            }
        }
    }
}
