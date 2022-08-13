CREATE TABLE [dbo].[tbl_SeriesGroup_Serie]
(
    [fk_SeriesGroup] BIGINT NOT NULL FOREIGN KEY REFERENCES tbl_SeriesGroup(Id),
    [fk_Serie] BIGINT NOT NULL FOREIGN KEY REFERENCES tbl_Serie(Id)
)
