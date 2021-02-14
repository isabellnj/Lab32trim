--
-- File generated with SQLiteStudio v3.2.1 on vi. ene. 22 20:08:00 2021
--
-- Text encoding used: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Table: __EFMigrationsHistory
CREATE TABLE "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);
INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) VALUES ('20210115194634_CreateDataBase', '5.0.2');
INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) VALUES ('20210115203755_DefaultCategory', '5.0.2');
INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) VALUES ('20210122185423_IsabelMS', '5.0.2');

-- Table: Categories
CREATE TABLE "Categories" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Categories" PRIMARY KEY AUTOINCREMENT,
    "Title" TEXT NULL
);
INSERT INTO Categories (Id, Title) VALUES (1, 'title');
INSERT INTO Categories (Id, Title) VALUES (2, 'title2');

-- Table: Posts
CREATE TABLE "Posts" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Posts" PRIMARY KEY AUTOINCREMENT,
    "Title" TEXT NULL,
    "Content" TEXT NULL,
    "CategoryId" INTEGER NULL, "DateTime" TEXT NULL,
    CONSTRAINT "FK_Posts_Categories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Categories" ("Id") ON DELETE RESTRICT
);

-- Table: PostTag
CREATE TABLE "PostTag" (
    "PostsId" INTEGER NOT NULL,
    "TagsId" INTEGER NOT NULL,
    CONSTRAINT "PK_PostTag" PRIMARY KEY ("PostsId", "TagsId"),
    CONSTRAINT "FK_PostTag_Posts_PostsId" FOREIGN KEY ("PostsId") REFERENCES "Posts" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_PostTag_Tags_TagsId" FOREIGN KEY ("TagsId") REFERENCES "Tags" ("Id") ON DELETE CASCADE
);

-- Table: Tags
CREATE TABLE "Tags" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Tags" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NULL
);

-- Index: IX_Posts_CategoryId
CREATE INDEX "IX_Posts_CategoryId" ON "Posts" ("CategoryId");

-- Index: IX_PostTag_TagsId
CREATE INDEX "IX_PostTag_TagsId" ON "PostTag" ("TagsId");

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
