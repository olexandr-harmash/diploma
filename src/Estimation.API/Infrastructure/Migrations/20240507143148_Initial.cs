﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estimation.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Criterions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criterions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estimations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estimations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstimateCriterions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    CriterionId = table.Column<Guid>(type: "uuid", nullable: false),
                    EstimateId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstimateCriterions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstimateCriterions_Criterions_CriterionId",
                        column: x => x.CriterionId,
                        principalTable: "Criterions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstimateCriterions_Estimations_EstimateId",
                        column: x => x.EstimateId,
                        principalTable: "Estimations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstimateCriterions_CriterionId",
                table: "EstimateCriterions",
                column: "CriterionId");

            migrationBuilder.CreateIndex(
                name: "IX_EstimateCriterions_EstimateId",
                table: "EstimateCriterions",
                column: "EstimateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstimateCriterions");

            migrationBuilder.DropTable(
                name: "Criterions");

            migrationBuilder.DropTable(
                name: "Estimations");
        }
    }
}
