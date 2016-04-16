
CREATE TABLE IF NOT EXISTS menu_item
(
	id uuid primary key
,	menu_id uuid not null
,	name varchar(100) not null
,	FOREIGN KEY (menu_id) REFERENCES menu (id)
);