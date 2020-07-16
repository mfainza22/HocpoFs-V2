<template>
  <div class="confirm-box-inline">
    <div v-if="!showConfirm" class="confirm-box__buttons children-mr-2">
      <ButtonSave @click="confirm()" />
      <ButtonCancel class="mr-2" @click="cancel()" />
    </div>
    <div v-else class="confirm-box__confirm">
      <label class="confirm-box__label">{{ confirmMsg }}</label>
      <ButtonYes @click="proceed()" />
      <ButtonNo ref="btnNo" @click="back()" />
    </div>
  </div>
</template>

<script>
export default {
  name: "ConfirmBoxInline",
  props: {
    beforeConfirmFunc: {
      type: Function,
      default: () => {}
    },
    afterConfirmFunc: {
      type: Function,
      default: () => {}
    },
    cancelFunc: {
      type: Function,
      default: () => {}
    }
  },
  data() {
    return {
      showConfirm: false,
      confirmMsg:
        "This will create new receiving record. Do you want to proced?",
      resolve: null
    };
  },
  methods: {
    onblur(e) {
      console.log(e);
    },
    onfocusin(e) {
      console.log(e);
    },
    onfocusout(e) {
      console.log(e);
    },
    confirm() {
      if (typeof this.beforeConfirmFunc !== "undefined")
        this.beforeConfirmFunc();
    },
    waitConfirm() {
      this.showConfirm = true;
      return new Promise(resolve => {
        this.resolve = resolve;
      });
    },
    back() {
      this.showConfirm = false;
      return this.resolve(false);
    },
    proceed() {
      this.showConfirm = false;
      return this.resolve(true);
    },
    cancel() {
      if (this.cancelFunc) this.cancelFunc();
    }
  }
};
</script>

<style lang="scss">
.confirm-box-inline {
  .confirm-box__buttons,
  .confirm-box__confirm {
    & > * {
      display: inline-flex;
    }
  }
  .confirm-box__label {
    margin-right: 12px;
  }
}
</style>
