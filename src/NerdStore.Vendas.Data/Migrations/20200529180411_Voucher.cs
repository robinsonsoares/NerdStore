using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NerdStore.Vendas.Data.Migrations
{
    public partial class Voucher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipoDescontoVoucher",
                table: "Vouchers",
                newName: "tipoDescontoVoucher");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataUtilizacao",
                table: "Vouchers",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tipoDescontoVoucher",
                table: "Vouchers",
                newName: "TipoDescontoVoucher");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataUtilizacao",
                table: "Vouchers",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
