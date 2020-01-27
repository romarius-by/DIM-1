--script to reset auto identity
--firts param - name of the table
--second param - function
--third param - value to what you want to reset

DBCC CHECKIDENT('UserProfile', RESEED, 0)