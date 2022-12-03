using CommerceBankApp.Areas.Identity.Data;
using CommerceBankApp.Controllers;
using CommerceBankApp.Data;
using CommerceBankApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Net.Http;
using System.Reflection.Emit;

namespace BankAppTesting.Moq
{
    public class PaymentInfo2ControllerTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManager;
        private readonly PaymentInfo2Controller _controller;

        public PaymentInfo2ControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;
            var _context = new ApplicationDbContext(options);
            _userManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            _controller = new PaymentInfo2Controller(_context, _userManager.Object);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        private PaymentInfo2 GetPaymentInfo2(int id)
        {
            var paymentinfo2 = new PaymentInfo2()
            {
                PaymentInfo2Id = id,
                PaymentInfo2Name = "testname",
                BankAccount = "100",
                Routing = "010",
                Address = "testadd",
                City = "testcity",
                State = "teststate",
                ZipCode = 64721
            };
            return paymentinfo2;
        }

        [Fact]
        public async Task Index_ActionExecutes_ReturnsViewForIndex()
        {
            var result = await _controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Details_IDisNull_ReturnsNotFoundResult()
        {
            var result = await _controller.Details(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_OrgisNull_ReturnsNotFoundResult()
        {
            var result = await _controller.Details(1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_IDisFound_ReturnsViewResult()
        {
            await _controller.Create(GetPaymentInfo2(1));
            var result = await _controller.Details(1);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_ActionExecutes_ReturnsViewForCreate()
        {
            var result = _controller.Create();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task CreateAsync_ModelStateisValid_ReturnsRedirectResultForCreate()
        {
            var result = await _controller.Create(GetPaymentInfo2(1));
            Assert.IsType<RedirectResult>(result);
        }

        [Fact]
        public async Task CreateAsync_InvalidModelState_ReturnsView()
        {
            _controller.ModelState.AddModelError("Routing", "Routing Number is required");
            var paymentinfo2 = new PaymentInfo2
            {
                PaymentInfo2Id = 1,
                PaymentInfo2Name = "testname",
                BankAccount = "100",
                Address = "testadd",
                City = "testcity",
                State = "teststate",
                ZipCode = 64721
            };
            var result = await _controller.Create(paymentinfo2);

            var viewResult = Assert.IsType<ViewResult>(result);
            var testPay = Assert.IsType<PaymentInfo2>(viewResult.Model);

            Assert.Equal(paymentinfo2.PaymentInfo2Id, testPay.PaymentInfo2Id);
        }

        [Fact]
        public async Task Edit_ActionExecutes_ReturnsViewResultForEdit()
        {
            await _controller.Create(GetPaymentInfo2(1));
            var result = await _controller.Edit(1);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Edit_IDisNull_ReturnsNotFoundResult()
        {
            var result = await _controller.Edit(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_OrgisNull_ReturnsNotFoundResult()
        {
            var result = await _controller.Edit(1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditFull_ActionExecutes_ReturnsRedirectResultForEdit()
        {
            var paymentinfo2 = GetPaymentInfo2(1);
            await _controller.Create(paymentinfo2);
            var result = await _controller.Edit(1, paymentinfo2);
            Assert.IsType<RedirectResult>(result);
        }

        [Fact]
        public async Task EditFull_IDisInvalid_ReturnsNotFoundResult()
        {
            var paymentinfo = GetPaymentInfo2(1);
            await _controller.Create(paymentinfo);
            var result = await _controller.Edit(2, paymentinfo);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditFull_ModelisInvalid_ReturnsViewResultForEdit()
        {
            _controller.ModelState.AddModelError("OrganizationName", "Name is required");
            var paymentinfo2 = new PaymentInfo2
            {


            };
            var result = await _controller.Edit(0, paymentinfo2);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Delete_ActionExecutes_ReturnsViewResultForDelete()
        {
            var paymentinfo = GetPaymentInfo2(1);
            await _controller.Create(paymentinfo);
            var result = await _controller.Delete(1);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Delete_IDisNull_ReturnsNotFoundResult()
        {
            var result = await _controller.Delete(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_OrgisNull_ReturnsNotFoundResult()
        {
            var result = await _controller.Delete(1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_ActionExecutes_ReturnsRedirectResultForDeleteConfirmed()
        {
            var paymentinfo2 = GetPaymentInfo2(1);
            await _controller.Create(paymentinfo2);
            var result = await _controller.DeleteConfirmed(1);
            Assert.IsType<RedirectResult>(result);
        }
    }
}
