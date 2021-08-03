<template>
  <div>
    <Header />
    <Menu @toggleMenuLeft="toggleMenuLeft" />
    <EmployeeContent :contentExpanded="contentExpanded" />
  </div>
</template>
<script>
import Header from "../../components/layout/TheHeader.vue";
import Menu from "../../components/layout/TheMenu.vue";
import EmployeeContent from "./EmployeeContent.vue";
export default {
  name: "Content",
  components: {
    Header,
    Menu,
    EmployeeContent,
  },
  props:{
    contentExpanded: Boolean,
  },
  methods: {
    toggleMenuLeft(contentExpanded){
      this.contentExpanded= contentExpanded;
    }
  },
  //$refs.AddEmployeeForm.showEm
};
</script>