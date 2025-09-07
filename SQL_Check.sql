SELECT *
  FROM [ahd].[dbo].[FT01]

SELECT *
  FROM [ahd].[dbo].[FT02]

SELECT top 10000 * 
  FROM [ahd].[dbo].[FT03]
  order by CreateAt desc

  SELECT *
  FROM [ahd].[dbo].[FT04]
  order by CreateAt desc

  select * from ScadaUser
  
  --truncate table ft01
  --truncate table ft02
  --truncate table ft03
  --truncate table ft04