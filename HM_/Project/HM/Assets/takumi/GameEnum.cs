using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneSound 
{
    //�^�C�g����ʂ̉��̔ԍ��̗񋓑�
    enum Title 
    {

        system,


    }

    //�Z���N�g��ʂ̉��̔ԍ��̗񋓑�
    enum Select 
    {
        system,


    }

    //�C���Q�[���Ŏg�����̗񋓑�
    enum main 
    {
        
        Dragon,
        Spider,
        system








    }
    /// <summary>
    /// ///////////////////////////////////////////////////////////////////////////
    /// </summary>
    enum Dragon 
    {
        DragonAttackHit,
        DragonJumpStart,
        DragonJumpEnd,
        DragonMove,
        DragonMoveVoice,
        DragonVoice,
        DragonLongAttack,
        DragonBite,
        DragonRoar,
        DragonBigRoar,
        DragonHitVoice,//�U����H�������̐�
        DragonNormalDown,
        DragonNormalDownVoice,
        DragonHardDown,//�_�E�����̕���
        DragonHardDownVoice,//�_�E�����̋��ѐ�
        //�����܂łǂ炲��

    }
    enum Spider 
    {
        SpiderAttackHIt,
        SpiderJumpStart,
        SpiderJumpSpace,
        SpiderJumpEnd,
        SpiderCaptorStart,
        SpiderCaptorSuccess,//�ߊl����
        SpiderCaptorMiss,
        SpiderTrapCreation,
        SpiderTrapSnagged,//�g���b�v�Ɉ�������������
        SpiderTrapLoop,//�g���b�v�Ɉ����������Ă��鎞�̉�
        SpiderTrapEnd,//�g���b�v�������Ȃ鉹
        SpiderMove,
        SpiderHitVoice,//�U����H�������̐�
        SpiderNormalDown,
        SpiderNormalDownVoice,
        SpiderHardDown,//�_�E�����̕���
        SpiderHardDownVoice,//�_�E�����̋��ѐ�
        //�����܂Œw�

    }

    enum System
    {
        BGM,
        Decision,
    }


}
