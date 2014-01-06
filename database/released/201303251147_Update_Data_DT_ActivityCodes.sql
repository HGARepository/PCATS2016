
UPDATE DT_ActivityCodes
set Deleted = 1 
FROM DT_ActivityCodes a
left join vwAllAccountCodes c on a.Code = c.Code
where c.code is null


UPDATE DT_ActivityCodes
set Name = c.Description
FROM DT_ActivityCodes a
left join vwAllAccountCodes c on a.Code = c.Code
where c.code is not null