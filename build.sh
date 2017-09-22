sudo systemctl stop dinnertime
dotnet build DinnerTime.sln -c "Release" -o /etc/dinnertime/
sudo systemctl start dinnertime

