USE [RSManpowerSchDb]
GO
/****** Object:  StoredProcedure [dbo].[spBudgetPCNExpense_ListAllByPCN]    Script Date: 3/21/2016 5:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spBudgetPCNExpense_ListAllByPCN]
@PCNID	int
AS
SELECT
	[ID],
	[PCNID],
	[Code],
	[Description],
	[DlrsPerItem],
	[NumItems],
	[MUPerc],
	[MarkUp],
	[TotalCost],
	[DeptGroup] -- Added 9/22/2015
FROM
	DT_BudgetPCNExpenses
WHERE
	[Deleted] = 0
	AND
	[PCNID] = @PCNID

	order by Code
