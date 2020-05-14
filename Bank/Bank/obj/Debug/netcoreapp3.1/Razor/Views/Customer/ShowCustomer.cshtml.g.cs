#pragma checksum "C:\Users\user\source\repos\ASPNETMVC2_Inlamningsuppgift\Bank\Bank\Views\Customer\ShowCustomer.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "668b9a4d0c05c02320e4452956840f4e2988efce"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"668b9a4d0c05c02320e4452956840f4e2988efce", @"/Views/Customer/ShowCustomer.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8154857fc4f6be7351a7b28979b40483d5730d10", @"/Views/_ViewImports.cshtml")]
    public class Views_Customer_ShowCustomer : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ShowCustomerDetailsViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ShowAccount", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Account", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
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
                                        <th scope=""col"">Frequency</th>
                                        <th scope=""col"">Created</th>
                                        <th scope=""col"">Account Balance</th>
                                        <th scope=""col"">Go to Account View</th>
                                    </tr>
                                </thead>
                                <tbody>                                    
");
#nullable restore
#line 29 "C:\Users\user\source\repos\ASPNETMVC2_Inlamningsuppgift\Bank\Bank\Views\Customer\ShowCustomer.cshtml"
                                     foreach (var account in Model.CustomerAccounts)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <tr>\r\n                                            <th scope=\"row\">");
#nullable restore
#line 32 "C:\Users\user\source\repos\ASPNETMVC2_Inlamningsuppgift\Bank\Bank\Views\Customer\ShowCustomer.cshtml"
                                                       Write(account.AccountId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                                            <td>");
#nullable restore
#line 33 "C:\Users\user\source\repos\ASPNETMVC2_Inlamningsuppgift\Bank\Bank\Views\Customer\ShowCustomer.cshtml"
                                           Write(account.Frequency);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            <td>");
#nullable restore
#line 34 "C:\Users\user\source\repos\ASPNETMVC2_Inlamningsuppgift\Bank\Bank\Views\Customer\ShowCustomer.cshtml"
                                           Write(account.Created);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            <td>");
#nullable restore
#line 35 "C:\Users\user\source\repos\ASPNETMVC2_Inlamningsuppgift\Bank\Bank\Views\Customer\ShowCustomer.cshtml"
                                           Write(account.Balance);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            <td>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "668b9a4d0c05c02320e4452956840f4e2988efce7938", async() => {
                WriteLiteral("View");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 36 "C:\Users\user\source\repos\ASPNETMVC2_Inlamningsuppgift\Bank\Bank\Views\Customer\ShowCustomer.cshtml"
                                                                                                       WriteLiteral(account.AccountId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</td>\r\n                                        </tr>\r\n");
#nullable restore
#line 38 "C:\Users\user\source\repos\ASPNETMVC2_Inlamningsuppgift\Bank\Bank\Views\Customer\ShowCustomer.cshtml"
                                    }

#line default
#line hidden
#nullable disable
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
