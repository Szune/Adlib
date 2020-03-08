# insert:
#$result = Invoke-WebRequest -Uri https://localhost:5001/api/ad/add/ -Method POST -ContentType "text/json" -Body "{ `"subject`": `"Madlibs for cheap`", `"body`": `"REAL cheap!`", `"priceSek`": 3, `"emailAddress`": `"mad@iwarson.org`" }";
#echo "StatusCode: $($result.StatusCode)`nContent:`n$($result.Content)"

# get:
#$result = Invoke-WebRequest -Uri https://localhost:5001/api/ad/get/timedesc -Method GET -ContentType "text/json";
#echo $result.Content

#$result = Invoke-WebRequest -Uri https://localhost:5001/api/ad/get/timeasc -Method GET -ContentType "text/json";
#echo $result.Content

# delete:
#$result = Invoke-WebRequest -Uri https://localhost:5001/api/ad/delete/2 -Method DELETE;
#echo $result.Content