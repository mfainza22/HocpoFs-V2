// const moment = require("moment");

// const e = {
//   DateStore: null,
//   isNull: value => {
//     return typeof value === undefined;
//   },
//   pad: (value, size) => {
//     var s = String(this);
//     while (s.length < (size || 2)) {
//       s = "0" + s;
//     }
//     return s;
//   },
//   getTimeString: value => {
//     return `${value.getHours().pad(2)}:
//       ${value.getMinutes().pad(2)}:
//       ${value.getSeconds().pad(2)}`;
//   }
// };

// export default e;

export default {};

export const objectUtil = {
  empty(obj) {
    console.log(obj);
    if (typeof obj === "undefined") return true;
    return Object.keys(obj).length == 0;
  }
};
