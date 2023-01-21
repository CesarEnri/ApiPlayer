CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Brands" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Description" text NOT NULL,
    "Active" boolean NOT NULL,
    "CreateAt" timestamp with time zone NOT NULL,
    "CreateBy" uuid NOT NULL,
    "UpdateAt" timestamp with time zone NULL,
    "UpdateBy" uuid NULL,
    CONSTRAINT "PK_Brands" PRIMARY KEY ("Id")
);

CREATE TABLE "Categories" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Description" text NOT NULL,
    "Content" text NOT NULL,
    "Active" boolean NOT NULL,
    "CreateAt" timestamp with time zone NOT NULL,
    "CreateBy" uuid NOT NULL,
    "UpdateAt" timestamp with time zone NULL,
    "UpdateBy" uuid NULL,
    CONSTRAINT "PK_Categories" PRIMARY KEY ("Id")
);

CREATE TABLE "ComplementTables" (
    "Id" uuid NOT NULL,
    "ReferenceGuid" uuid NOT NULL,
    "TableReference" text NOT NULL,
    "NameColumn" text NOT NULL,
    "TypeColumn" text NOT NULL,
    "ValueColumn" text NOT NULL,
    "Active" boolean NOT NULL,
    "CreateAt" timestamp with time zone NOT NULL,
    "CreateBy" uuid NOT NULL,
    "UpdateAt" timestamp with time zone NULL,
    "UpdateBy" uuid NULL,
    CONSTRAINT "PK_ComplementTables" PRIMARY KEY ("Id")
);

CREATE TABLE "Prices" (
    "Id" uuid NOT NULL,
    "Value" numeric NOT NULL,
    "Discount" numeric NOT NULL,
    "Quantity" integer NOT NULL,
    "Active" boolean NOT NULL,
    "CreateAt" timestamp with time zone NOT NULL,
    "CreateBy" uuid NOT NULL,
    "UpdateAt" timestamp with time zone NULL,
    "UpdateBy" uuid NULL,
    CONSTRAINT "PK_Prices" PRIMARY KEY ("Id")
);

CREATE TABLE "Qualities" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Description" text NOT NULL,
    "Content" text NOT NULL,
    "Active" boolean NOT NULL,
    "CreateAt" timestamp with time zone NOT NULL,
    "CreateBy" uuid NOT NULL,
    "UpdateAt" timestamp with time zone NULL,
    "UpdateBy" uuid NULL,
    CONSTRAINT "PK_Qualities" PRIMARY KEY ("Id")
);

CREATE TABLE "Taxes" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Description" text NOT NULL,
    "Value" numeric NOT NULL,
    "Active" boolean NOT NULL,
    "CreateAt" timestamp with time zone NOT NULL,
    "CreateBy" uuid NOT NULL,
    "UpdateAt" timestamp with time zone NULL,
    "UpdateBy" uuid NULL,
    CONSTRAINT "PK_Taxes" PRIMARY KEY ("Id")
);

CREATE TABLE "Vendors" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Description" text NOT NULL,
    "Active" boolean NOT NULL,
    "CreateAt" timestamp with time zone NOT NULL,
    "CreateBy" uuid NOT NULL,
    "UpdateAt" timestamp with time zone NULL,
    "UpdateBy" uuid NULL,
    CONSTRAINT "PK_Vendors" PRIMARY KEY ("Id")
);

CREATE TABLE "SubCategories" (
    "Id" uuid NOT NULL,
    "CategoryId" uuid NOT NULL,
    "Name" text NOT NULL,
    "Description" text NOT NULL,
    "Content" text NOT NULL,
    "Active" boolean NOT NULL,
    "CreateAt" timestamp with time zone NOT NULL,
    "CreateBy" uuid NOT NULL,
    "UpdateAt" timestamp with time zone NULL,
    "UpdateBy" uuid NULL,
    CONSTRAINT "PK_SubCategories" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_SubCategories_Categories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Categories" ("Id") ON DELETE CASCADE
);

CREATE TABLE "TaxPriceDetails" (
    "Id" uuid NOT NULL,
    "PriceId" uuid NOT NULL,
    "TaxId" uuid NOT NULL,
    "Active" boolean NOT NULL,
    "CreateAt" timestamp with time zone NOT NULL,
    "CreateBy" uuid NOT NULL,
    "UpdateAt" timestamp with time zone NULL,
    "UpdateBy" uuid NULL,
    CONSTRAINT "PK_TaxPriceDetails" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_TaxPriceDetails_Prices_PriceId" FOREIGN KEY ("PriceId") REFERENCES "Prices" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_TaxPriceDetails_Taxes_TaxId" FOREIGN KEY ("TaxId") REFERENCES "Taxes" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Products" (
    "Id" uuid NOT NULL,
    "SubCategoryId" uuid NOT NULL,
    "BrandId" uuid NOT NULL,
    "VendorId" uuid NOT NULL,
    "PriceId" uuid NOT NULL,
    "Name" text NOT NULL,
    "Description" text NOT NULL,
    "Sold" boolean NOT NULL,
    "Active" boolean NOT NULL,
    "CreateAt" timestamp with time zone NOT NULL,
    "CreateBy" uuid NOT NULL,
    "UpdateAt" timestamp with time zone NULL,
    "UpdateBy" uuid NULL,
    CONSTRAINT "PK_Products" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Products_Brands_BrandId" FOREIGN KEY ("BrandId") REFERENCES "Brands" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Products_Prices_PriceId" FOREIGN KEY ("PriceId") REFERENCES "Prices" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Products_SubCategories_SubCategoryId" FOREIGN KEY ("SubCategoryId") REFERENCES "SubCategories" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Products_Vendors_VendorId" FOREIGN KEY ("VendorId") REFERENCES "Vendors" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Products_BrandId" ON "Products" ("BrandId");

CREATE INDEX "IX_Products_PriceId" ON "Products" ("PriceId");

CREATE INDEX "IX_Products_SubCategoryId" ON "Products" ("SubCategoryId");

CREATE INDEX "IX_Products_VendorId" ON "Products" ("VendorId");

CREATE INDEX "IX_SubCategories_CategoryId" ON "SubCategories" ("CategoryId");

CREATE INDEX "IX_TaxPriceDetails_PriceId" ON "TaxPriceDetails" ("PriceId");

CREATE INDEX "IX_TaxPriceDetails_TaxId" ON "TaxPriceDetails" ("TaxId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230121152958_InitialCreate', '7.0.2');

COMMIT;

