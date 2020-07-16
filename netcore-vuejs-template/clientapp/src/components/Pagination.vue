<template>
  <ul class="pagination">
    <li>
      <span>{{ getPageStart }} - {{ getPageLast }} of {{ itemCount }}</span>
    </li>
    <li>
      <button class="pagination__nav" @click="prev()">
        <i class="icon-prev" />
      </button>
    </li>
    <!-- <li v-for="(pc, i) in pageCount" :key="i">
      <button class="pagination__item" @click="updateValue(pc)">
        {{ pc }}
      </button>
    </li> -->
    <li>
      <button class="pagination__nav" @click="next()">
        <i class="icon-next" />
      </button>
    </li>
  </ul>
</template>

<script>
export default {
  name: "Pagination",
  props: {
    value: {
      type: Number,
      default: 1
    },
    pageCount: {
      type: Number,
      default: 1
    },
    pageSize: {
      type: Number,
      default: 50
    },
    itemCount: {
      type: Number,
      default: 0
    }
  },
  computed: {
    getActualLastDiff() {
      return this.value * this.pageSize - this.itemCount;
    },
    getPageStart() {
      if (this.pageCount == 0) {
        return 0;
      }
      if (this.value <= 1) {
        return 1;
      } else if (this.value > 1 && this.value < this.pageCount) {
        return this.getPageLast - this.pageSize + 1;
      } else {
        return this.getPageLast - this.pageSize + 1 + this.getActualLastDiff;
      }
    },
    getPageLast() {
      let lr = this.value * this.pageSize;
      lr = lr > this.itemCount ? this.itemCount : lr;
      return lr;
    }
  },
  methods: {
    prev() {
      let value = this.value - 1;
      value = value <= 0 ? 1 : value;
      this.updateValue(value);
    },
    next() {
      let value = this.value + 1;
      value = value > this.pageCount ? this.pageCount : value;
      this.updateValue(value);
    },
    updateValue(value) {
      this.$emit("input", value);
      // this.setActive(value);
    },
    setActive(value) {
      const piActiveCLS = "pagination__item--active";

      const pi = document
        .querySelector(".pagination")
        .getElementsByClassName("pagination__item");

      for (let i = 0; i <= pi.length - 1; i++) {
        pi[i].classList.remove(piActiveCLS);
      }
      pi[value - 1].classList.add(piActiveCLS);
    }
  }
};
</script>

<style lang="scss">
i[class*="icon-"]::before {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  vertical-align: middle;
}
i.icon-prev::before {
  font: normal normal normal 24px "Material Design Icons";
  content: "\F0141";
  position: relative;
  display: inline-flex;
}

i.icon-next::before {
  font: normal normal normal 24px "Material Design Icons";
  content: "\F0142";
}

.pagination {
  list-style-type: none;
  & > li {
    display: inline-block;
  }

  &__nav,
  &__item {
    outline: none;
  }

  &__item {
    display: inline-flex;
    align-items: center;
    justify-content: center;
    vertical-align: middle;
    width: 24px;
    color: #3c3c3c;
  }

  &__item--active {
    background: #363636;
    color: white;
    transition: background 0.3s;
  }
}
</style>
