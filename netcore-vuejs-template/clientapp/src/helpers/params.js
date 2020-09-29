export default {
  objectToParams: obj => {
    var d = obj;
    var r =
      "?" +
      Object.keys(d)
        .map(function(a) {
          let r2 = "";
          if (typeof d[a] == "object") {
            if (hasOwnProperty.call(d[a], "length")) {
              for (let i = 0; i <= d[a].length - 1; i++) {
                r2 += encodeURI(`${a}=${encodeURI(d[a][i])}&`);
              }
            } else {
              r2 = encodeURI(`${a}=${encodeURI(d[a])}`);
            }
          } else {
            r2 = encodeURI(`${a}=${encodeURI(d[a])}`);
          }
          return r2;
        })
        .join("&");
    return encodeURI(r);
  },
  arrayToParams: (arr, key) => {
    if (arr === undefined) return "";
    if (arr.length === 0) return "";
    var r =
      "?" +
      arr
        .map(function(e) {
          return key + "=" + e;
        })
        .join("&");

    return r;
  }
};
