cd /d C:\Program Files\RabbitMQ Server\rabbitmq_server-4.2.2\sbin

call rabbitmq-plugins enable rabbitmq_management

call rabbitmqctl add_user rabbit_admin rabbit_admin_Pass
call rabbitmqctl set_permissions -p / rabbit_admin ".*" ".*" ".*"
call rabbitmqctl set_user_tags rabbit_admin administrator

call rabbitmqctl add_user contracts_creator contrat_cr_Pass_rabbit
call rabbitmqctl set_permissions -p / contracts_creator ".*" ".*" ".*"

call rabbitmqctl add_user contracts_cheker contracts_ch_Pass_rabbit
call rabbitmqctl set_permissions -p / contracts_cheker ".*" ".*" ".*"

pause