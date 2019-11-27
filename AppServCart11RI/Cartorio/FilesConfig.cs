using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AppServCart11RI.Cartorio
{
    public class FilesConfig
    {
        private readonly string _file_path_modelo_doc = @"App_Data\Arquivos\Modelos\";
        private readonly string _file_path_ri_base = @"App_Data\Arquivos\Reg_Imoveis\Base\";
        private readonly string _file_path_ri_em_escrita = @"App_Data\Arquivos\Reg_Imoveis\EmEscrita\";
        private readonly string _file_path_ri_finalizados = @"App_Data\Arquivos\Reg_Imoveis\Finalizados\";
        private readonly string _file_ri_base_name = @"ri_modelo_base.docx";

        public FilesConfig()
        {
            //
        }

        public string FilePathModeloDoc {
            get { return this._file_path_modelo_doc; }
        }

        public string FilePathRiBase
        {
            get { return this._file_path_ri_base; }
        }
        public string FilePathRiEmEscrita
        {
            get { return this._file_path_ri_em_escrita; }
        }
        public string FilePathRiFinalizados
        {
            get { return this._file_path_ri_finalizados; }
        }
        public string FileRiBaseName
        {
            get { return this._file_ri_base_name; }
        }
            
        public string GetModeloDocFileName(long IdModeloDoc)
        {
            return Path.Combine(this.FilePathModeloDoc,  "modelo_" + IdModeloDoc + ".docx");
        }
    }
}