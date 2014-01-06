USE [RSManpowerSchDb_dev]
GO

/****** Object:  StoredProcedure [dbo].[spRPRT_ResourceVariance]    Script Date: 06/04/2013 10:32:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spRPRT_ResourceVariance]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spRPRT_ResourceVariance]
GO

USE [RSManpowerSchDb_dev]
GO

/*
EXEC [dbo].[spRPRT_ResourceVariance] 0
EXEC [dbo].[spRPRT_ResourceVariance] 1
*/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spRPRT_ResourceVariance]
@IsPipeline int
AS

SELECT
	d.Name AS [SortVal]
	,ps.AcctGroup AS [DeptGroup]
	,fms.[ProjectType] AS [Indust]
	,proj.Number AS [ProjNumber]
	,proj.Description AS [ProjTitle]
	,cust.Name AS [Customer]
	,ROUND(ps.JSRmn,0) AS JSRmn
	,ROUND(ps.MPRmn,0) AS MPRmn
	,ROUND(ps.budTot,0) AS budTot
	,ROUND(ps.ForToCmp,0) AS ForToCmp
	,w.EndOfWeek
FROM
	(
	SELECT
		ps.ProjID
		,ps.AcctGroup
		,SUM(ps.budTot) AS [budTot]
		,SUM(ps.JSRmn) AS [JSRmn]
		,SUM(ps.MPRmn) AS [MPRmn]
		,SUM(ps.ForToCmp) AS [ForToCmp]
	FROM
		vwAllProjectStatusByDept ps
	GROUP BY
		ps.AcctGroup, ps.ProjID
	) ps
	LEFT JOIN
	DT_Projects proj ON ps.ProjID = proj.ID
	LEFT JOIN
	DT_Employees pm ON proj.ProjMngrID = pm.ID
	LEFT JOIN
	DT_Customers cust ON proj.CustomerID = cust.ID
	LEFT JOIN
	(
	SELECT
		AcctGroup
		,[Name]
	FROM
		DT_Departments
	WHERE
		[Deleted] = 0
		AND
		[ID] <> 9
	) d ON ps.AcctGroup = d.AcctGroup 
	LEFT JOIN
	(
	SELECT
		0 AS [ProjAdd]
		,[EndOfWeek]
	FROM
		SY_WeekLists 
	WHERE
		GETDATE() BETWEEN [StartOfWeek] AND [EndOfWeek]
	) w ON ps.[ProjID] > w.[ProjAdd]
	LEFT JOIN
	dt_fmsprojectdata fms ON proj.[Number] = fms.[CProject]
WHERE
	ps.JSRmn + ps.MPRmn + ps.budTot + ps.ForToCmp > 0
	AND
	CHARINDEX('X',proj.Number) < 1
	AND
	proj.[Deleted] = 0
	AND
	-- remove the pipeline or not pipeline
	( 
	 (@IsPipeline = 0 AND (LEFT(proj.[Number],2) <> '8.' AND LEFT(proj.[Number],3) <> 'P.8') )
	 OR
	 (@IsPipeline = 1 AND (LEFT(proj.[Number],2) = '8.' OR LEFT(proj.[Number],3) = 'P.8') )
	)
ORDER BY
	ps.AcctGroup ASC
	,proj.Number ASC

GO


