using Contacto.Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contacto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedContactCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            List<ContactCategory> categories = [ContactCategory.ContactCategoryWork, ContactCategory.ContactCategoryCustom, ContactCategory.ContactCategoryPrivate];
            var categoryValuesStatement = categories.Select(c => $"('{c.Id.ToString()}','{c.Title}',{(c.CustomCategory ? 1 : 0)})");
            var categoriesSql = string.Format("insert into ContactCategories values{0};", string.Join(',', categoryValuesStatement));

            List<ContactSubCategory> subCategories = [ContactCategory.ContactSubCategoryClient, ContactCategory.ContactSubCategoryBoss, ContactCategory.ContactSubCategoryCoworker];
            var subCategoryValuesStatement = subCategories.Select(c => $"('{c.Id.ToString()}','{c.Title}','{c.CategoryId}')");
            var subCategoriesSql = string.Format("insert into ContactSubCategories values{0};", string.Join(',', subCategoryValuesStatement));

            migrationBuilder.Sql($"{categoriesSql}\n{subCategoriesSql}");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            List<ContactCategory> categories = [ContactCategory.ContactCategoryWork, ContactCategory.ContactCategoryCustom, ContactCategory.ContactCategoryPrivate];
            var categoryStatement = string.Join(',', categories.Select(c => $"'{c.Id}'"));
            migrationBuilder.Sql(string.Format(@"delete from ContactCategories where Id in ({0})", categoryStatement));

            List<ContactSubCategory> subCategories = [ContactCategory.ContactSubCategoryClient, ContactCategory.ContactSubCategoryBoss, ContactCategory.ContactSubCategoryCoworker];
            var subCategoryValuesStatement = string.Join(',', subCategories.Select(c => $"'{c.Id}'"));
            migrationBuilder.Sql(string.Format(@"delete from ContactSubCategories where Id in ({0})", subCategoryValuesStatement));
        }
    }
}
