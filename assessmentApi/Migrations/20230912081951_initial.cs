using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace assessmentApi.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AOTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    History = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Boundary = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Log = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Cache = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Notify = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Identifier = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    premium = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_AOTable_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Form",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatebookId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AddChangeDeleteFlag = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: true),
                    SubSequence = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))"),
                    Type = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    FormType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinOccurs = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    MaxOccurs = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((99))"),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    HelpText = table.Column<string>(type: "ntext", nullable: true),
                    Condition = table.Column<string>(type: "ntext", nullable: true),
                    HidePremium = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    TemplateFile = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Hidden = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    TabCondition = table.Column<string>(type: "ntext", nullable: true),
                    TabResourceName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BtnResAdd = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BtnResModify = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BtnResDelete = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BtnResViewDetail = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BtnResRenumber = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BtnResView = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BtnResCopy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BtnCndAdd = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BtnCndModify = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BtnCndDelete = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BtnCndViewDetail = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BtnCndRenumber = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BtnCndView = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BtnCndCopy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BtnLblAdd = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BtnLblModify = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BtnLblDelete = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BtnLblViewDetail = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BtnLblRenumber = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BtnLblView = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BtnLblCopy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_Form_Id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_Form_Table",
                        column: x => x.TableId,
                        principalTable: "AOTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_AOTable_Name",
                table: "AOTable",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "ix_Form_RatebookId",
                table: "Form",
                column: "RatebookId");

            migrationBuilder.CreateIndex(
                name: "ix_Form_TableId",
                table: "Form",
                column: "TableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Form");

            migrationBuilder.DropTable(
                name: "AOTable");
        }
    }
}
