#pragma checksum "C:\Users\user\source\repos\ASPNETMVC2_Inlamningsuppgift\Bank\Bank\Views\Customer\ShowCustomer.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7a4e44a37e7ea3886018c542a4e01087c3c3cfbf"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customer_ShowCustomer), @"mvc.1.0.view", @"/Views/Customer/ShowCustomer.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\user\source\repos\ASPNETMVC2_Inlamningsuppgift\Bank\Bank\Views\_ViewImports.cshtml"
using Bank;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\user\source\repos\ASPNETMVC2_Inlamningsuppgift\Bank\Bank\Views\_ViewImports.cshtml"
using Bank.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\user\source\repos\ASPNETMVC2_Inlamningsuppgift\Bank\Bank\Views\_ViewImports.cshtml"
using Bank.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7a4e44a37e7ea3886018c542a4e01087c3c3cfbf", @"/Views/Customer/ShowCustomer.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8154857fc4f6be7351a7b28979b40483d5730d10", @"/Views/_ViewImports.cshtml")]
    public class Views_Customer_ShowCustomer : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ShowCustomerDetailsViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<section class=""generic-banner relative"">
    <div class=""container"">
        <div class=""row height align-items-center justify-content-center"">
            <div class=""col-lg-10"">
                <div class=""generic-banner-content"">

                    <h2 class=""text-white"">Customer Details</h2>

                    <label>Name</label>
                    <p class=""text-white"">");
#nullable restore
#line 12 "C:\Users\user\source\repos\ASPNETMVC2_Inlamningsuppgift\Bank\Bank\Views\Customer\ShowCustomer.cshtml"
                                     Write(Model.Givenname);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 12 "C:\Users\user\source\repos\ASPNETMVC2_Inlamningsuppgift\Bank\Bank\Views\Customer\ShowCustomer.cshtml"
                                                      Write(Model.Surname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    <label>Total balance on all accounts</label>\r\n                    <p class=\"text-white\">");
#nullable restore
#line 14 "C:\Users\user\source\repos\ASPNETMVC2_Inlamningsuppgift\Bank\Bank\Views\Customer\ShowCustomer.cshtml"
                                     Write(Model.TotalAmountOnAccounts);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" kr</p>
                    <label>All Accounts</label>
                    <div class=""row"">

                            <table class=""table"">
                                <thead>
                                    <tr>
                                        <th scope=""col"">#</th>
                                        <th scope=""col"">Account Balance</th>
                                        <th scope=""col"">Go to Account View</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    Hej
");
            WriteLiteral("                                </tbody>\r\n                            </table>\r\n                        </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</section>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ShowCustomerDetailsViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591