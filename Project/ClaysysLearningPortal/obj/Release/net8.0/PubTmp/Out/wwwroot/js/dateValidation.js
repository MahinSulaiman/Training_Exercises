
    var todayDate=new Date();
    var year=todayDate.getUTCFullYear();
    var maxYear=year-18;
    var minYear=year-50;
    var month=todayDate.getUTCMonth()+1;
    var day=todayDate.getDate();
    if(month<10){
        month = "0" + month;
        }
    if(day<10){
        day = "0" + day;
        }
    var maxDate = `${maxYear}-${month}-${day}`;
    var minDate = `${minYear}-${month}-${day}`;

    document.getElementById("dob").setAttribute("max",maxDate);
    document.getElementById("dob").setAttribute("min", minDate);
