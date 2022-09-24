using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommerceBankApp.Data.Migrations
{
    public partial class initialsetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonationType",
                columns: table => new
                {
                    donationTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    donationTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    donationTypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationType", x => x.donationTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    organizationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    organizationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    donationGoal = table.Column<float>(type: "real", nullable: false),
                    currentDonated = table.Column<float>(type: "real", nullable: false),
                    organizationDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    donationTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.organizationID);
                    table.ForeignKey(
                        name: "FK_Organization_DonationType_donationTypeID",
                        column: x => x.donationTypeID,
                        principalTable: "DonationType",
                        principalColumn: "donationTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    accountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    donorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.accountID);
                });

            migrationBuilder.CreateTable(
                name: "Donor",
                columns: table => new
                {
                    donorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    billingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    homeAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    accountID = table.Column<int>(type: "int", nullable: false),
                    donorInfoID = table.Column<int>(type: "int", nullable: false),
                    paymentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donor", x => x.donorID);
                    table.ForeignKey(
                        name: "FK_Donor_Account_accountID",
                        column: x => x.accountID,
                        principalTable: "Account",
                        principalColumn: "accountID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "DonorInfo",
                columns: table => new
                {
                    donorInfoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cardExpiration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cvcNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bankRoutingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    donorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorInfo", x => x.donorInfoID);
                    table.ForeignKey(
                        name: "FK_DonorInfo_Donor_donorID",
                        column: x => x.donorID,
                        principalTable: "Donor",
                        principalColumn: "donorID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    paymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    donatedAmount = table.Column<float>(type: "real", nullable: false),
                    donorID = table.Column<int>(type: "int", nullable: false),
                    organizationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.paymentID);
                    table.ForeignKey(
                        name: "FK_Payment_Donor_donorID",
                        column: x => x.donorID,
                        principalTable: "Donor",
                        principalColumn: "donorID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Payment_Organization_organizationID",
                        column: x => x.organizationID,
                        principalTable: "Organization",
                        principalColumn: "organizationID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_donorID",
                table: "Account",
                column: "donorID");

            migrationBuilder.CreateIndex(
                name: "IX_Donor_accountID",
                table: "Donor",
                column: "accountID");

            migrationBuilder.CreateIndex(
                name: "IX_Donor_donorInfoID",
                table: "Donor",
                column: "donorInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_Donor_paymentID",
                table: "Donor",
                column: "paymentID");

            migrationBuilder.CreateIndex(
                name: "IX_DonorInfo_donorID",
                table: "DonorInfo",
                column: "donorID");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_donationTypeID",
                table: "Organization",
                column: "donationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_donorID",
                table: "Payment",
                column: "donorID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_organizationID",
                table: "Payment",
                column: "organizationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Donor_donorID",
                table: "Account",
                column: "donorID",
                principalTable: "Donor",
                principalColumn: "donorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donor_DonorInfo_donorInfoID",
                table: "Donor",
                column: "donorInfoID",
                principalTable: "DonorInfo",
                principalColumn: "donorInfoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donor_Payment_paymentID",
                table: "Donor",
                column: "paymentID",
                principalTable: "Payment",
                principalColumn: "paymentID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Donor_donorID",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_DonorInfo_Donor_donorID",
                table: "DonorInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Donor_donorID",
                table: "Payment");

            migrationBuilder.DropTable(
                name: "Donor");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "DonorInfo");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "DonationType");
        }
    }
}
