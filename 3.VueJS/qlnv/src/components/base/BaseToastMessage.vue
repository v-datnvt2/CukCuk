<template>
  <div>
    <div class="toastmessage" v-if="!isHiddenToastMessage">
      <div :class="['toast-icon', toastIcon]"></div>
      <div class="toast-text">{{ toastMessage.toastText }}</div>
      <div
        :class="['toast-close', toastIconClose]"
        @click="btnCloseOnClick"
      ></div>
    </div>
  </div>
</template>
<script>
export default {
  name: "ToastMessage",
  props: {
    toastMessage: Object,
    isHiddenToastMessage: Boolean,
  },
  data() {
    return {
      toastIcon: "",
      toastIconClose: "",
    };
  },

  methods: {
    btnCloseOnClick() {
      this.$emit("btn-close");
    },

    updateToastMessage() {
      let vm = this;
      console.log(this.toastMessage.toastType);
      switch (this.toastMessage.toastType) {
        case "Danger":
          vm.toastIcon = "icon-toast-danger";
          vm.toastIconClose = "icon-red-cross";
          break;
        case "Warning":
          vm.toastIcon = "icon-toast-warning";
          vm.toastIconClose = "icon-orange-cross";
          break;
        case "Notify":
          vm.toastIcon = "icon-toast-check";
          vm.toastIconClose = "icon-green-cross";
          break;
        case "Inform":
          vm.toastIcon = "icon-toast-i";
          vm.toastIconClose = "icon-blue-cross";
          break;
        default:
          vm.toastIcon = "icon-toast-check";
          vm.toastIconClose = "icon-green-cross";
          break;
      }

      setTimeout(() => {
        vm.btnCloseOnClick()
      }, 3000);
    },
  },
  /**
   * Chưa làm..  todo
   */
  mounted() {
this.updateToastMessage()
  },
  watch:{
    toastMessage:function () { 
      this.updateToastMessage()
     }
  }
};
</script>