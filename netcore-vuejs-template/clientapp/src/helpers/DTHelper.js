const moment = require("moment");
class DTHelper {
  constructor(date) {
    this._date = date;
    if (typeof this._date == "undefined") this._date = new Date();
  }

  raw() {
    return this._date;
  }

  get date() {
    return moment(this._date).format("yyyy-MM-DD");
  }
  set date(value) {
    this._date = value + " " + this.time;
  }

  get time() {
    return moment(this._date).format("HH:mm:ss");
  }
  set time(value) {
    let dt = this.date + " " + value;
    console.log(dt);
    this._date = moment(dt).format();
  }

  get formattedDate() {
    return moment(this._date).format("MMM-DD-yyyy");
  }

  get formattedTime() {
    return moment(this._date).format("HH:mm");
  }

  get formattedTime2() {
    return moment(this._date).format("HH:mm:ss");
  }

  get fullFormattedDate() {
    return moment(this._date).format("MMM-DD-yyyy HH:mm:ss");
  }

  get jsDate() {
    return moment(this._date).format();
  }

  format(format) {
    return moment(this._date).format(format);
  }
}

export default DTHelper;
