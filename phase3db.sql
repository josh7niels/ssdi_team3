CREATE TABLE question_Forum 
(
    forum_id int AUTO_INCREMENT PRIMARY KEY,
    mem_id varchar(50) ,
    content text not null,
    post_date date,
    post_time time,
    FOREIGN KEY(mem_id) REFERENCES login(u_id)
);

CREATE TABLE reply_Forum 
(
    r_forum_id int AUTO_INCREMENT PRIMARY KEY,
    p_forum_id int ,
    mem_id varchar(50) ,
    content text not null,
    post_date date,
    post_time time,
    FOREIGN key(p_forum_id) REFERENCES question_forum(forum_id),
    FOREIGN KEY(mem_id) REFERENCES login(u_id)
);

insert into question_Forum(mem_id,content,post_date,post_time)
VALUES
('mnaga@uncc.edu','what is the dosage of this medicine',CURRENT_DATE,CURRENT_TIME),
('llopaudr@uncc.edu','what is the reason for headache',CURRENT_DATE,CURRENT_TIME);

insert into reply_Forum(p_forum_id,mem_id,content,post_date,post_time)
VALUES
(3,'llopaudr@uncc.edu','3 per day',CURRENT_DATE,CURRENT_TIME),
(3,'mBilliska@uncc.edu','take 2 pills a day after meal',CURRENT_DATE,CURRENT_TIME);