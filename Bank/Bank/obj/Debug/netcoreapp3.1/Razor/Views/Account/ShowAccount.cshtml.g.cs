#pragma checksum "C:\Users\user\source\repos\ASPNETMVC2_Inlamningsuppgift\Bank\Bank\Views\Account\ShowAccount.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1e1348647f0177c433351d919bd00d915fbe8afe"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_ShowAccount), @"mvc.1.0.view", @"/Views/Account/ShowAccount.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1e1348647f0177c433351d919bd00d915fbe8afe", @"/Views/Account/ShowAccount.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8154857fc4f6be7351a7b28979b40483d5730d10", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_ShowAccount : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ShowAccountDetailsViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("color:black;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CreateTransaction", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Transaction", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
<script src=""https://cdnjs.cloudflare.com/ajax/libs/knockout/3.5.1/knockout-latest.min.js""></script>
<script src=""https://cdnjs.cloudflare.com/ajax/libs/knockout.mapping/2.4.1/knockout.mapping.min.js""></script>

<section class=""generic-banner relative"">
    <div class=""container"">
        <div class=""main-wrapper"">
            <!-- Start service Area -->
            <section class=""service-area pt-100"" id=""feature"">
                <div class=""container"">
                    <h2 class=""text-white"">Account Details</h2>
                    <div class=""row"">
                        <div class=""col"">
                        </div>
                        <div class=""col"">
                            <div class=""single-service"">
                                <label>Account ID</label>
                                <p class=""text-white"">");
#nullable restore
#line 19 "C:\Users\user\source\repos\ASPNETMVC2_Inlamningsuppgift\Bank\Bank\Views\Account\ShowAccount.cshtml"
                                                 Write(Model.AccountId);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                            </div>
                        </div>
                        <div class=""col"">
                            <div class=""single-service"">
                                <label>Balance</label>
                                <p class=""text-white"">");
#nullable restore
#line 25 "C:\Users\user\source\repos\ASPNETMVC2_Inlamningsuppgift\Bank\Bank\Views\Account\ShowAccount.cshtml"
                                                 Write(Model.Balance.ToString("### ### ##0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@" SEK</p>
                            </div>
                        </div>
                        <div class=""col"">
                        </div>
                    </div>

                    <button class=""genric-btn default circle text-uppercase mb-30"">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1e1348647f0177c433351d919bd00d915fbe8afe6367", async() => {
                WriteLiteral("Create Transaction on Account");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 32 "C:\Users\user\source\repos\ASPNETMVC2_Inlamningsuppgift\Bank\Bank\Views\Account\ShowAccount.cshtml"
                                                                                                                                                                         WriteLiteral(Model.AccountId);

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
            WriteLiteral("</button>\r\n");
            WriteLiteral(@"
                </div>
            </section>
        </div>
    </div>
</section>

<!-- About Generic Start -->
<div class=""main-wrapper"">
    <!-- Start service Area -->
    <section class=""service-area pt-100"" id=""feature"">
        <div class=""container"">

            <h3>Transactions</h3>

            <div class=""row"">

                <table class=""table"">
                    <thead>
                        <tr>
                            <th scope=""col"">#</th>
                            <th scope=""col"">Date</th>
                            <th scope=""col"">Type</th>
                            <th scope=""col"">Operation</th>
                            <th scope=""col"">Amount</th>
                            <th scope=""col"">Balance</th>
                            <th scope=""col"">Notes</th>
                            <th scope=""col"">Bank</th>
                            <th scope=""col"">Account</th>
                        </tr>
                    </thead>
               ");
            WriteLiteral(@"     <tbody data-bind=""foreach: transactions"">
                        <tr>
                            <th scope=""row"" data-bind=""text: transactionId""></th>
                            <td data-bind=""text: date""></td>
                            <td data-bind=""text: type""></td>
                            <td data-bind=""text: operation""></td>
                            <td data-bind=""text: amount""></td>
                            <td data-bind=""text: balance""> SEK</td>
                            <td data-bind=""text: symbol""></td>
                            <td data-bind=""text: bank""></td>
                            <td data-bind=""text: account""></td>                            
                        </tr>
                    </tbody>
                </table>
            </div>
");
            WriteLiteral(@"            <div class=""row justify-content-center align-items-start"">
                <button class=""primary-btn text-uppercase mb-10"" data-bind=""click: load20More"">Load more</button>
            </div>
        </div>
    </section>
</div>
<!-- End service Area -->
<!-- Load More Area -->


");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n    <script>\r\n\r\n        var transactionViewModel = function (data) {\r\n                self = this;\r\n                ko.mapping.fromJS(data, {}, this);\r\n\r\n            self.skip = ko.observable(0);\r\n            self.id = ko.observable(");
#nullable restore
#line 100 "C:\Users\user\source\repos\ASPNETMVC2_Inlamningsuppgift\Bank\Bank\Views\Account\ShowAccount.cshtml"
                               Write(Model.AccountId);

#line default
#line hidden
#nullable disable
                WriteLiteral(@");

            this.load20More = function () {

                self.skip(self.skip() + 20);
                $.ajax({
                    url: ""/Account/GetFrom?startPos="" + self.skip() + ""&accountId="" + self.id(), success: function (result) {
                        //result
                        for (var i = 0; i < result.length; i++) {
                            self.transactions.push(result[i]);
                        }
                    }
                });
                }
            }

            var model = ");
#nullable restore
#line 116 "C:\Users\user\source\repos\ASPNETMVC2_Inlamningsuppgift\Bank\Bank\Views\Account\ShowAccount.cshtml"
                   Write(Html.Raw(Json.Serialize(Model)));

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n            //var viewModel = ko.mapping.fromJS(model,mapping);\r\n            var viewModel = new transactionViewModel(model);\r\n            ko.applyBindings(viewModel);\r\n    </script>\r\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ShowAccountDetailsViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
