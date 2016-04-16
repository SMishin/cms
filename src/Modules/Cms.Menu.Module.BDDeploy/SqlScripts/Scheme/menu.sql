
CREATE TABLE IF NOT EXISTS menu
(
	id uuid primary key
,	parent_id uuid references menu(id)
,	name varchar(100)	
);