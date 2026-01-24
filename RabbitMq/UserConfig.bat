cd /d C:\Program Files\RabbitMQ Server\rabbitmq_server-4.2.2\sbin

call rabbitmq-plugins enable rabbitmq_management

call rabbitmqctl add_user rabbit_admin rabbit_admin_Pass
call rabbitmqctl set_permissions -p / rabbit_admin ".*" ".*" ".*"
call rabbitmqctl set_user_tags rabbit_admin administrator

call rabbitmqctl add_user contracts_creator contrat_cr_Pass_rabbit
call rabbitmqctl set_permissions -p / contracts_creator ".*" ".*" ".*"

call rabbitmqctl add_user contracts_cheker contracts_ch_Pass_rabbit
call rabbitmqctl set_permissions -p / contracts_cheker ".*" ".*" ".*"

call rabbitmqadmin --vhost "/" exchanges declare --name "to.checker" --type "topic" --durable true
call rabbitmqadmin --vhost "/" queues declare --name "contract.created.queue" --type "classic" --durable true --auto-delete false
call rabbitmqadmin --vhost "/" queues declare --name "contract.updated.queue" --type "classic" --durable true --auto-delete false
call rabbitmqadmin --vhost="/" bindings declare --source="to.checker" --destination-type="queue" --destination="contract.created.queue" --routing-key="contract.created"
call rabbitmqadmin --vhost="/" bindings declare --source="to.checker" --destination-type="queue" --destination="contract.updated.queue" --routing-key="contract.updated"

call rabbitmqadmin --vhost "/" exchanges declare --name "to.creator" --type "direct" --durable true
call rabbitmqadmin --vhost "/" queues declare --name "contract.checked.queue" --type "classic" --durable true --auto-delete false
call rabbitmqadmin --vhost="/" bindings declare --source="to.creator" --destination-type="queue" --destination="contract.checked.queue" --routing-key="contract.checked"

pause