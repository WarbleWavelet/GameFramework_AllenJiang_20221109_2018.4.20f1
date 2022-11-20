//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.DataTable;
using GameFramework.Event;
using System;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace StarForce
{
    public class ProcedureMenu : ProcedureBase
    {
        private bool m_StartGame = false;
        private MenuForm m_MenuForm = null;

        public override bool UseNativeDialog
        {
            get
            {
                return false;
            }
        }

        public void StartGame()
        {
            m_StartGame = true;
        }
                                                         
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            m_StartGame = false;
            GameEntry.UI.OpenUIForm(UIFormId.MenuForm, this);

            Test();



        }



        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            if (m_MenuForm != null)
            {
                m_MenuForm.Close(isShutdown);
                m_MenuForm = null;
            }
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (m_StartGame)
            {
                procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Main"));
                procedureOwner.SetData<VarByte>("GameMode", (byte)GameMode.Survival);
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            m_MenuForm = (MenuForm)ne.UIForm.Logic;
        }


        /// <summary>
        /// 测试
        /// </summary>
        private void Test()
        {
            // Test_01();
            //  Test_02();
            //Test_03();

        }

        private void Test_01()
        {
            new FsmTemp();

        }

        private void Test_02()
        {
            IDataTable<DRAircraft> a = GameEntry.DataTable.GetDataTable<DRAircraft>();
            DRAircraft aircraft = a.GetDataRow(10000);
            Log.Info("飞机测试ID：" + aircraft.TestId);

        }

        private void Test_03()
        {
            IDataTable<DRScene> a = GameEntry.DataTable.GetDataTable<DRScene>();
            DRScene scene1 = a.GetDataRow(id: 1);
            DRScene scene2 = a.GetDataRow(id: 2);
            foreach (var item in scene1.Positions)
            {
                Log.Info("Scene Positions：" + item);
            }
            foreach (var item in scene2.Positions)
            {
                Log.Info("Scene Positions：" + item);
            }
        }
    }
}
