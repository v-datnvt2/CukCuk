<template>
  <div id="app">

    <EmployeePage />
  </div>
</template>

<script>

import EmployeePage from "./view/employee/EmployeePage.vue";
// import EmployeePage from "./view/temp/parent.vue";
export default {
  name: "App",
  components: {
   EmployeePage,
  },
};
</script>

<style>
@import "./css/main.css";
</style>
