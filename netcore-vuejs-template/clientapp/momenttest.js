var moment = require("moment");

var fd = moment(new Date()).format("Y-MM-DD hh:mm:ss Z ");
console.log(fd);
const fd2 = new Date(fd);

console.log(fd2);

console.log(moment(fd2).format("Y-MM-DD hh:mm:ss Z "));
